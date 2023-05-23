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
</script>
<template>
  <ul class="space-y-2">
    <li v-for="car in cars"
        :key="car.id"
        class="flex gap-6 text-lg items-center">
      <span>
        <Hash class="inline text-slate-400 -top-0.5 relative"/>
        {{ car.registrationNumber }}
      </span>
      <span>
        <MapPin class="inline text-slate-400"/>
        {{ car.departmentCity }}
      </span>
      <Delete class="inline text-red-500 hover:text-red-800"/>
    </li>
  </ul>
</template>