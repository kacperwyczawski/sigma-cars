<script setup>
const route = useRoute();

const {data} = await useFetch(
    "/api/car-types"
    + `?start-date=${route.query.startDate}`
    + `&end-date=${route.query.endDate}`,
);

const carTypes = data.value.carTypes;
</script>
<template>
  <main class="p-4">
    <div v-if="carTypes === undefined"
         class="mt-16 text-center text-4xl text-slate-500">
      Sorry, no cars available for the selected dates and location.
    </div>
    <div v-else>
      <ul class="grid grid-cols-1 lg:grid-cols-2 2xl:grid-cols-3 gap-2">
        <li v-for="carType in carTypes"> <!--TODO: add keys to all v-fors-->
          <CarTypeCard :car="carType"/>
        </li>
      </ul>
    </div>
  </main>
</template>