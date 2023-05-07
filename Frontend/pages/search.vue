<script setup>
const route = useRoute();

const state = reactive({
    cars: null,
});

async function fetchData() {
    console.log(`fetching cars for dates ${route.query.startDate} to ${route.query.endDate}`);

    const res = await useLazyApi(`car-models`
        + `?start-date=${route.query.startDate}`
        + `&end-date=${route.query.endDate}`);

    if (!res.data.value) {
        const errorRes = res.error.value.data;
        throw createError({
            statusCode: errorRes.status,
            statusMessage: errorRes.title,
            message: errorRes.detail,
        })
    }

    state.cars = res.data;
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