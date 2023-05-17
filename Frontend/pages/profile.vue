<script setup>
import { XCircle } from "lucide-vue-next";

const userData = useUserData();

const tempRentals = [
    {
        carMake: "BMW",
        carModel: "M3",
        carRegistrationNumber: "ABC123",
        startDate: "2021-10-10",
        endDate: "2021-10-20",
    },
    {
        carMake: "Audi",
        carModel: "A4",
        carRegistrationNumber: "DEF456",
        startDate: "2021-10-10",
        endDate: "2021-10-20",
    },
    {
        carMake: "Mercedes",
        carModel: "C63",
        carRegistrationNumber: "GHI789",
        startDate: "2021-10-10",
        endDate: "2021-10-20",
    },
];

</script>
<template>
    <div v-if="userData === null"
         class="mt-16 text-center text-4xl text-slate-500 animate-pulse">
        Not logged in
    </div>
    <div v-else
         class="p-4">
        <div class="rounded-md p-4 flex justify-between bg-gray-200">
            <p class="text-xl font-bold">
                {{ userData.firstName }} {{ userData.lastName }}
            </p>
            <p>
                {{ userData.email }}
            </p>
        </div>
        <div class="rounded-md border p-4 mt-4">
            <h2 class="text-xl">
                Your rentals:
            </h2>
            <ul class="mt-4 divide-y">
                <li v-for="rental in tempRentals"
                    class="flex py-2 flex-col md:flex-row md:justify-between">
                    <div class="text-slate-800 font-bold">
                        {{ rental.carMake }} {{ rental.carModel }}
                        <span class="text-xs text-gray-500 font-normal">
                            {{ rental.carRegistrationNumber }}
                        </span>
                    </div>
                    <div>
                        <span class="text-slate-600">
                            from
                        </span>
                        {{ rental.startDate }}
                        <span class="text-slate-600">
                            to
                        </span>
                        {{ rental.endDate }}
                        <span class="ml-4 underline text-orange-600">
                            Cancel
                        </span>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</template>