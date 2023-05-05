<script setup>
const route = useRoute();

const state = reactive({
    cars: null,
});

async function fetchData() {
    state.cars = null; // idk why, but without this data won't be fetched after route change
    const {data: carsResult} = await useFetch(`http://localhost/api/car-models`
        + `?start-date=${route.query.startDate}`
        + `&end-date=${route.query.endDate}`);
    state.cars = carsResult;
    console.log(`data fetched for dates ${route.query.startDate} to ${route.query.endDate}`);
}

watch(
    () => route.query,
    () => fetchData(),
    {immediate: true},
);

</script>
<template>
    <main class="p-4 max-w-6xl mx-auto">
        <ClientOnly>
            <h1 v-if="state.cars === null"
                class="mt-16 text-center text-4xl text-slate-500 animate-pulse">
                Loading...
            </h1>
            <h1 v-else-if="state.cars.length === 0"
                class="mt-16 text-center text-4xl text-slate-500">
                No cars available for the selected dates and location.
            </h1>
            <ul v-else
                class="flex flex-col gap-2">
                <li v-for="car in state.cars">
                    <CarCard :car="car"/>
                </li>
            </ul>
        </ClientOnly>
    </main>
</template>