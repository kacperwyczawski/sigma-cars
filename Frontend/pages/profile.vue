<script setup>
import {ShieldCheck, Plus} from "lucide-vue-next";

const userData = useUserData();
const router = useRouter();

if (userData.value === null) {
    router.push('/login');
}

const {data} = await useFetch(`/api/users/${userData.value.userId}/rentals`);

let departments = [];
if (userData.value.role === "admin") {
    const {data} = await useFetch(`/api/departments`);
    departments = data.value === undefined
        ? []
        : data.value;
} // TODO: refresh departments on add (using watch and maybe useAsyncData)

const rentals = data.value.rentals === undefined
    ? []
    : data.value.rentals;

function formatDate(date) {
    date = new Date(date);
    return date.toDateString();
}

rentals.forEach(rental => {
    rental.startDate = formatDate(rental.startDate);
    rental.endDate = formatDate(rental.endDate);
});

function handleLogout() {
    userData.value = null;
    router.push('/');
}

const adding = ref(false);
const newDepartment = ref({
    countryCode: '',
    city: '',
    address: '',
});

async function handleAddDepartment() {
    adding.value = false;
    await useFetch(`/api/departments`, {
        method: 'POST',
        body: newDepartment.value,
    });
}
</script>
<template>
    <div>
        <div v-if="userData === null"
             class="mt-16 text-center text-4xl text-slate-500 animate-pulse">
            Not logged in
        </div>
        <div v-else
             class="p-4">
            <div class="rounded-md p-4 flex flex-col md:flex-row justify-between bg-gray-200">
                <div class="text-xl font-bold flex gap-2 items-center">
                    <h1>
                        {{ userData.firstName }} {{ userData.lastName }}
                    </h1>
                    <ShieldCheck
                            v-if="userData.role === 'admin'"
                            class="h-6 relative text-orange-600"/>
                </div>
                <p>
                    {{ userData.email }}
                    <span class="block md:inline md:ml-4">
                        <ButtonCancel @click="handleLogout">Logout</ButtonCancel>
                    </span>
                </p>
            </div>
            <div v-if="rentals.length !== 0"
                 class="rounded-md border p-4 mt-4">
                <h2 class="text-xl">
                    Your rentals:
                </h2>
                <ul class="mt-4 divide-y">
                    <li v-for="rental in rentals"
                        class="flex py-2 flex-col md:flex-row md:justify-between">
                        <div class="font-bold">
                            {{ rental.carMake }} {{ rental.carModel }}
                            <span class="text-xs text-slate-500 font-normal">{{ rental.carRegistrationNumber }}</span>
                        </div>
                        <div>
                            <span class="text-slate-500">from</span>
                            {{ rental.startDate }}
                            <span class="text-slate-500">to</span>
                            {{ rental.endDate }}
                            <span class="text-slate-500">in</span>
                            [Location]
                            <span class="block md:inline md:ml-4"><ButtonCancel>Cancel</ButtonCancel></span>
                        </div>
                    </li>
                </ul>
            </div>
            <div v-if="userData.role === 'admin'"
                 class="rounded-md border p-4 mt-4">
                <div class="flex justify-between">
                    <h2 class="text-xl">
                        Departments:
                    </h2>
                    <button v-if="!adding"
                            class="hover:bg-orange-600 text-white bg-slate-800 transition-colors duration-75 rounded-md py-1 px-2 text-sm"
                            @click="adding = true">
                        Add
                    </button>
                </div>
                <ul class="mt-4 divide-y">
                    <li v-if="adding">
                        <form class="caret-orange-600"
                              @submit.prevent="handleAddDepartment">
                            <label for="countryCode">
                                Country code:
                                <input type="text"
                                       id="countryCode"
                                       maxlength="2"
                                       required
                                       class="w-10 border rounded-full m-2 ml-0 focus:outline-none focus:border-orange-600 px-2"
                                       v-model="newDepartment.countryCode">
                            </label>
                            <label for="city">
                                City:
                                <input type="text"
                                       id="city"
                                       required
                                       class="w-32 border rounded-full m-2 ml-0 focus:outline-none focus:border-orange-600 px-2"
                                       v-model="newDepartment.city">
                            </label>
                            <label for="address">
                                Address:
                                <input type="text"
                                       id="address"
                                       required
                                       class="w-32 border rounded-full m-2 ml-0 focus:outline-none focus:border-orange-600 px-2"
                                       v-model="newDepartment.address">
                            </label>
                            <input type="submit"
                                   value="Add"
                                   class="underline text-orange-600 hover:text-orange-800 ml-2">
                        </form>
                    </li>
                    <li v-for="department in departments"
                        class="flex py-2 flex-col md:flex-row md:justify-between">
                        <div class="flex items-center gap-2">
                            <div class="rounded-full border px-1.5 text-slate-500 text-sm">
                                {{ department.countryCode }}
                            </div>
                            <div class="font-bold">
                                {{ department.city }}
                            </div>
                            <div class="text-slate-500">
                                {{ department.address }}
                            </div>
                        </div>
                        <div>
                            <ButtonCancel>Remove</ButtonCancel>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>