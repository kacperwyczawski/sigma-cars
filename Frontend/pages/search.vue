<script setup>
import {ShieldCheck} from "lucide-vue-next";
import {Switch} from "@headlessui/vue";

const route = useRoute();
const userData = useUserData();

const availableOnly = ref(true);
const addingCarType = ref(false);
const newCarType = reactive({
  make: '',
  model: '',
  productionYear: 0,
  color: '',
  pricePerDay: 0,
  seatCount: 0,
});

const {data: carTypes, refresh} = await useAsyncData(
    () =>
        $fetch("/api/car-types", {
          query: {
            "start-date": route.query.startDate,
            "end-date": route.query.endDate,
            "available-only": availableOnly.value,
          },
        }),
    {
      watch: [availableOnly],
      transform: (data) => data.carTypes,
    },
);

function handleAddCarModel() {
  addingCarType.value = false;
  useFetch("/api/car-types", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: newCarType,
  });
}
</script>
<template>
  <main class="p-2 flex flex-col gap-2">
    <div class="flex gap-2 flex-wrap">
      <InputPrimary
          placeholder="Search"
          class="flex-grow w-full md:w-32"/>
      <div class="rounded bg-slate-800 p-2 text-white flex-grow md:flex-grow-0">
        Placeholder
      </div>
      <div class="rounded bg-slate-800 p-2 text-white flex-grow md:flex-grow-0">
        Placeholder
      </div>
      <div class="rounded bg-slate-800 p-2 text-white flex-grow md:flex-grow-0">
        Placeholder
      </div>
      <div class="rounded bg-slate-800 p-2 text-white flex-grow md:flex-grow-0">
        Placeholder
      </div>
      <div
          v-if="userData && userData.role === 'admin'"
          class="rounded bg-slate-800 p-2 text-white flex-grow md:flex-grow-0 flex items-center">
        <Switch
            v-model="availableOnly"
            :class="availableOnly ? 'bg-orange-600' : 'bg-slate-400'"
            class="relative inline-flex h-5 w-11 items-center rounded-full">
          <span
              :class="availableOnly ? 'translate-x-6' : 'translate-x-1'"
              class="inline-block h-4 w-4 transform rounded-full bg-white transition"/>
        </Switch>
        <span class="ml-2">Available only</span>
      </div>
      <ButtonPrimary
          v-if="userData && userData.role === 'admin'"
          class="flex items-center gap-2 flex-grow md:flex-grow-0"
          :show-arrow="false"
          @click="addingCarType = true">
        <ShieldCheck class="inline h-5"/>
        Add car type
      </ButtonPrimary>
      <Modal :show="addingCarType">
        <template #header>
          Add car type
        </template>
        <template #body>
          <form class="-m-2"
                @submit.prevent="handleAddCarModel">
            <div class="grid md:grid-cols-2 gap-4 p-2 max-w-xl">
              <label>
                Make:
                <InputSecondary
                    v-model="newCarType.make"
                    class="w-full"
                    type="text"
                    maxlength="50"
                    required/>
              </label>
              <label>
                Model:
                <InputSecondary
                    v-model="newCarType.model"
                    class="w-full"
                    type="text"
                    maxlength="50"
                    required/>
              </label>
              <label>
                Production year:
                <InputSecondary
                    v-model="newCarType.productionYear"
                    class="w-full"
                    type="number"
                    min="0"
                    max="3000"
                    required/>
              </label>
              <label>
                Color:
                <InputSecondary
                    v-model="newCarType.color"
                    class="w-full"
                    type="text"
                    maxlength="50"
                    required/>
              </label>
              <label>
                Price per day:
                <InputSecondary
                    v-model="newCarType.pricePerDay"
                    class="w-full"
                    type="number"
                    min="0"
                    max="1000"
                    required/>
              </label>
              <label>
                Seat count:
                <InputSecondary
                    v-model="newCarType.seatCount"
                    class="w-full"
                    type="number"
                    min="0"
                    max="100"
                    required/>
              </label>
            </div>
            <div class="flex justify-end p-2 mt-2 gap-2 border-t">
              <InputButtonPrimary
                  @click="addingCarType = false"
                  type="button"
                  value="Cancel"/>
              <InputButtonPrimary
                  value="Add"/>
            </div>
          </form>
        </template>
      </Modal>
    </div>
    <div v-if="carTypes === undefined"
         class="mt-16 text-center text-4xl text-slate-500">
      Sorry, no cars available for the selected dates and location.
    </div>
    <div v-else>
      <ul class="grid grid-cols-1 lg:grid-cols-2 2xl:grid-cols-3 gap-2">
        <li v-for="carType in carTypes"
            :key="carType.id"> <!--TODO: add keys to all v-fors-->
          <CarTypeCard :car="carType"
          @delete="refresh"/>
        </li>
      </ul>
    </div>
  </main>
</template>