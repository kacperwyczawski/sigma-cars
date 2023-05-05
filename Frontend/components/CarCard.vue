<script setup>
import {User, Hourglass, CalendarCheck2} from "lucide-vue-next";

const props = defineProps({
    car: Object,
});
</script>
<template>
    <div class="border max-w-6xl divide-y">
        <div class="flex justify-between text-slate-800 font-bold p-2 text-2xl">
            <h1>{{ car.make }} {{ car.model }}</h1>
            <div>${{ car.pricePerDay }}</div>
        </div>
        <div class="grid sm:grid-cols-2 p-2 gap-2">
            <div class="aspect-video bg-gray-200 animate-pulse"></div>
            <div class="flex flex-col justify-between text-xl">
                <div class="space-y-3 text-slate-600">
                    <p class="flex gap-2">
                        <User class="inline h-7"/>
                        {{ car.seatCount }} seats
                    </p>
                    <p class="flex gap-2">
                        <Hourglass class="inline h-7"/>
                        from {{ car.productionYear }}
                    </p>
                    <p class="flex gap-2">
                        <CalendarCheck2 class="inline h-7"/>
                        {{ car.availableCarIds.length }} cars available
                    </p>
                </div>
                <NuxtLink :to="'/cars/' + car.id"
                    class="group p-2 hover:bg-orange-600 text-center text-white bg-slate-800 mt-2">
                    Details
                    <span class="group-hover:translate-x-2 inline-block transition-transform">
                        -&gt;
                    </span>
                </NuxtLink>
            </div>
        </div>
    </div>
</template>