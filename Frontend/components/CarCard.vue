<script setup>
import {User} from "lucide-vue-next";

const props = defineProps({
    car: Object,
});

</script>
<template>
    <div class="border divide-y rounded-md">
        <div class="flex justify-between text-slate-800 font-bold text-2xl p-2">
            <h1>{{ car.make }} {{ car.model }}</h1>
            <div>${{ car.pricePerDay }}</div>
        </div>
        <div class="p-2">
            <div class="aspect-video bg-gray-200 animate-pulse rounded-md"/>
        </div>
        <div class="text-slate-600 text-xl p-2 flex justify-between items-center">
            <p>
                <User class="inline h-7"/>
                {{ car.seatCount }} seats
            </p>
            <ButtonArrow>
                Rent now
            </ButtonArrow>
        </div>
    </div>
</template>