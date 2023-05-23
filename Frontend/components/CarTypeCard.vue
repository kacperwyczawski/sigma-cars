<script setup>
import {User, SlidersHorizontal} from "lucide-vue-next";
import {useUserData} from "~/composables/useUserData";
import ButtonPrimary from "~/components/ButtonPrimary.vue";

const props = defineProps({
  car: Object,
});

const userData = useUserData();
const router = useRouter();
const route = useRoute();

const startDate = computed(() => {
  let date = new Date(route.query.startDate);
  date.setHours(10);
  return date.toISOString();
});

const endDate = computed(() => {
  let date = new Date(route.query.endDate);
  date.setHours(10);
  return date.toISOString();
});

function handleRent() {
  if (!userData.value) {
    // TODO: Tell user they need to be logged in via modal or something
    router.push('/auth/login');
    return;
  }

  useFetch(`/api/car-types/${props.car.id}/rentals`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: {
      userId: userData.value.userId,
      startDate: startDate.value,
      endDate: endDate.value,
    },
  });

  router.push('/profile');
}

const {data: departments} = await useFetch('/api/departments');
const showDetails = ref(false);
const addingCar = ref(false);
const newCar = reactive({
  registrationNumber: '',
  departmentId: 1,
});

async function handleAddCar() {
  addingCar.value = false;
  await useFetch(`/api/car-types/${props.car.id}/cars`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: {
      registrationNumber: newCar.registrationNumber.toUpperCase(),
      departmentId: newCar.departmentId,
    },
  });
  newCar.registrationNumber = "";
  newCar.departmentId = 1;
}
</script>
<template>
  <div class="border divide-y rounded-md">
    <div class="flex justify-between text-slate-800 font-bold text-2xl p-2">
      <h1>{{ car.make }} {{ car.model }}</h1>
      <div>${{ car.pricePerDay }}</div>
    </div>
    <div class="p-2">
      <div class="aspect-video bg-slate-200 animate-pulse rounded-md"/>
    </div>
    <div class="text-slate-600 text-xl p-2 flex justify-between items-center">
      <div class="flex gap-1">
        <User class="h-7"/>
        {{ car.seatCount }} seats
      </div>
      <div class="flex gap-2 items-center">
        <button
            v-if="userData && userData.role === 'admin'"
            @click="showDetails = true"
            class="bg-slate-100 rounded basis-6 p-2 hover:bg-slate-200 transition-colors duration-75 block">
          <SlidersHorizontal/>
          <Modal :show="showDetails">
            <template #header>
              {{ car.make }} {{ car.model }} details
            </template>
            <template #body>
              <CarTypeDetails
                  :carTypeId="car.id"
                  :key="addingCar"/>
              <form class="caret-orange-600 mt-2 pt-2 border-t border-dashed flex flex-col gap-2"
                    v-if="addingCar"
                    @submit.prevent="handleAddCar">
                <label>
                  Registration number:
                  <InputSecondary
                      type="text"
                      required
                      v-model="newCar.registrationNumber"
                      maxlength="10"
                      class="w-[7.2rem] font-mono"/>
                </label>
                <label>
                  Department:
                  <select v-model="newCar.departmentId"
                          required
                          class="rounded-full border focus: bg-white px-2 h-6">
                    <option v-for="department in departments"
                            :key="department.departmentId"
                            :value="department.departmentId">
                      {{ department.city }}
                    </option>
                  </select>
                </label>
                <div class="space-x-2">
                  <input type="submit"
                         value="Add"
                         class="underline text-orange-600 hover:text-orange-800">
                  <input type="button"
                         value="Cancel"
                         @click="addingCar = false"
                         class="underline text-orange-600 hover:text-orange-800">
                </div>

              </form>
            </template>
            <template #footer>
              <ButtonPrimary
                  v-if="!addingCar"
                  @click="addingCar = true"
                  :show-arrow="false">
                Add
              </ButtonPrimary>
              <ButtonPrimary
                  :show-arrow="false">
                Delete
              </ButtonPrimary>
              <ButtonPrimary
                  @click="showDetails = false; addingCar = false"
                  :show-arrow="false">
                Close
              </ButtonPrimary>
            </template>
          </Modal>
        </button>
        <ButtonPrimary @click="handleRent">
          Rent now
        </ButtonPrimary>
      </div>
    </div>
  </div>
</template>