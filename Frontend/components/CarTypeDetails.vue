<script setup lang="ts">
import {Hash, MapPin, Delete} from "lucide-vue-next"

const props = defineProps<{
  carTypeId: number
}>();

const {data: cars} = await useFetch(
    `/api/car-types/${props.carTypeId}/cars`,
    {
      transform: (data: any) => data.cars
    }
);

async function handleDeleteCar(id: number) {
  // TODO: create endpoint for deleting car directly, not through car type
  await useFetch(`/api/car-types/${props.carTypeId}/cars/${id}`, {
    method: "DELETE"
  });
  cars.value = cars.value.filter((car: any) => car.id !== id);
}
</script>
<template>
  <ul class="space-y-2"> <!--TODO: this should be table-->
    <li v-for="car in cars"
        :key="car.id"
        class="flex gap-6 text-lg items-center justify-between">
      <span>
        <Hash class="inline text-slate-400 -top-0.5 relative"/>
        {{ car.registrationNumber }}
      </span>
      <span>
        <MapPin class="inline text-slate-400"/>
        {{ car.departmentCity }}
      </span>
      <Delete
          @click="handleDeleteCar(car.id)"
          class="inline text-red-500 hover:text-red-800"/>
    </li>
  </ul>
</template>