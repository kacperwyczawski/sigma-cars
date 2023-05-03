<script setup>
const route = useRoute();

const state = reactive({
    cars: null,
});

async function fetchData() {
    const {data: carsResult} = await useFetch(`http://localhost/api/car-models`
        + `?start-date=${route.query.startDate}`
        + `&end-date=${route.query.endDate}`);
    state.cars = carsResult;
}

onMounted(fetchData);

</script>
<template>
    <main class="p-4">
        <h1 v-if="state.cars === null" class="mt-16 text-center text-4xl text-slate-500 animate-pulse">
            Loading...
        </h1>
        <ul v-else class="flex flex-col justify-center gap-2">
            <li v-for="car in state.cars">
                <CarCard :car="car"/>
            </li>
        </ul>
    </main>
</template>