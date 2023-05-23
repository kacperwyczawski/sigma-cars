<script setup>
import {ShieldCheck} from "lucide-vue-next";
import InputPrimary from "~/components/InputPrimary.vue";
import {Switch} from "@headlessui/vue";

const route = useRoute();
const userData = useUserData();

const availableOnly = ref(true);

const {data: carTypes} = await useAsyncData(
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
</script>
<template>
  <main class="p-2 flex flex-col gap-2">
    <div class="flex gap-2">
      <InputPrimary
          placeholder="Search"
          class="flex-grow"/>
      <div class="rounded bg-slate-800 p-2 text-white">
        Placeholder
      </div>
      <div class="rounded bg-slate-800 p-2 text-white">
        Placeholder
      </div>
      <div class="rounded bg-slate-800 p-2 text-white">
        Placeholder
      </div>
      <div class="rounded bg-slate-800 p-2 text-white">
        Placeholder
      </div>
      <div 
          v-if="userData && userData.role === 'admin'"
          class="rounded bg-slate-800 p-2 text-white flex items-center">
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
          class="flex items-center gap-2"
          :show-arrow="false">
        <ShieldCheck class="inline h-5"/>
        Add car type
      </ButtonPrimary>
    </div>
    <div v-if="carTypes === undefined"
         class="mt-16 text-center text-4xl text-slate-500">
      Sorry, no cars available for the selected dates and location.
    </div>
    <div v-else>
      <ul class="grid grid-cols-1 lg:grid-cols-2 2xl:grid-cols-3 gap-2">
        <li v-for="carType in carTypes"
            :key="carType.id"> <!--TODO: add keys to all v-fors-->
          <CarTypeCard :car="carType"/>
        </li>
      </ul>
    </div>
  </main>
</template>