<script setup>
import {User} from "lucide-vue-next";
import {useUserData} from "~/composables/useUserData";

const props = defineProps({
    car: Object,
});

const userData = useUserData();
const router = useRouter();
const route = useRoute();

const startDate = computed(() => {
    let date = new Date(route.query.startDate);
    date.setHours(10);
    return date.toISOString();
});

const endDate = computed(() => {
    let date = new Date(route.query.endDate);
    date.setHours(10);
    return date.toISOString();
});

function handleRent() {
    if (!userData.value) {
        // TODO: Tell user they need to be logged in via modal or something
        router.push('/login');
        return;
    }
    
    useFetch(`/api/car-types/${props.car.id}/rentals`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: {
            userId: userData.value.userId,
            startDate: startDate.value,
            endDate: endDate.value,
        },
    });
    
    router.push('/profile');
}

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
            <ButtonArrow @click="handleRent">
                Rent now
            </ButtonArrow>
        </div>
    </div>
</template>