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
            <button class="group px-4 py-2 hover:bg-orange-600 text-center text-white bg-slate-800 transition-colors duration-75 rounded-md">
                Rent now
                <span class="group-hover:translate-x-2 inline-block transition-transform">
                        -&gt;
                    </span>
            </button>
        </div>
    </div>
</template>