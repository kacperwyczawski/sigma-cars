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
  <table
      v-if="cars && cars.length !== 0"
      class="w-full">
    <tr
        v-for="car in cars"
        :key="car.id"
        class="flex p-2 even:bg-gray-100 rounded-md">
      <td class="pl-2 rotate-180">
        <Delete
            @click="handleDeleteCar(car.id)"
            class="text-red-500 hover:text-red-700"/>
      </td>
      <td>
        <Hash class="text-slate-400"/>
      </td>
      <td class="pr-4">
        {{ car.registrationNumber }}
      </td>
      <td>
        <MapPin class="text-slate-400"/>
      </td>
      <td>
        {{ car.departmentCity }}
      </td>
    </tr>
  </table>
  <p v-else
     class="text-lg text-center text-slate-500">
    No cars registered
  </p>
</template>