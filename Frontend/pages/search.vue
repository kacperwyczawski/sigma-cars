<script setup>

const route = useRoute();

const {pending, data} = await useLazyFetch( // data can be watched, see https://nuxt.com/docs/getting-started/data-fetching#example-1
    "api/car-models"
    + `?start-date=${route.query.startDate}`
    + `&end-date=${route.query.endDate}`
    + "&available-only=false",
);

</script>
<template>
    <main class="p-4">
        <div v-if="pending"
             class="mt-16 text-center text-4xl text-slate-500 animate-pulse">
            Loading...
        </div>
        <div v-else-if="data.carModels.length === 0"
             class="mt-16 text-center text-4xl text-slate-500 animate-pulse">
            Sorry, no cars available for the selected dates and location.
        </div>
        <div v-else>
            <ul class="grid grid-cols-1 lg:grid-cols-2 2xl:grid-cols-3 gap-2">
                <li v-for="car in data.carModels">
                    <CarCard :car="car"/>
                </li>
            </ul>
        </div>
    </main>
</template>