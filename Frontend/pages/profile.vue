<script setup lang="ts">
import {ShieldCheck, RefreshCcw} from "lucide-vue-next";
import {Ref} from "vue";
import {Department} from "~/types/Department";
import {Rental} from "~/types/Rental";

const userData = useUserData();
const router = useRouter();

if (userData.value === null) {
  router.push('/auth/login');
}

const {data: rentals, refresh: refreshRentals} = await useFetch(
    `/api/users/${userData.value?.userId}/rentals`, {
      transform: (data: any) => {
        let result = data.rentals === undefined
            ? []
            : data.rentals;

        function formatDate(date: string | Date) {
          date = new Date(date);
          return date.toDateString();
        }

        result.forEach((rental: Rental) => {
          rental.startDate = formatDate(rental.startDate);
          rental.endDate = formatDate(rental.endDate);
        });
        
        return result;
      },
    },
);

const departments: Ref<Department[]> = ref([]);
if (userData.value?.role === "admin") {
  const {data: newDepartments} = await useFetch<Department[]>("/api/departments");
  departments.value = newDepartments?.value ?? [];
}

function handleLogout() {
  userData.value = null;
  router.push('/');
}

const adding = ref(false);
const newDepartment = reactive({
  countryCode: '',
  city: '',
  address: '',
});

async function handleAddDepartment() {
  adding.value = false;
  await useFetch(`/api/departments`, {
    method: 'POST',
    body: newDepartment,
  });
  departments.value.push(newDepartment as Department);
}

async function handleCancelRent(id: number) {
  await useFetch(`/api/rentals/${id}`, {
    method: 'DELETE',
  });
  rentals.value = rentals.value.filter((r: Rental) => r.id !== id);
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
          {{ userData?.email }}
          <span class="block md:inline md:ml-4">
                        <ButtonSecondary @click="handleLogout">Logout</ButtonSecondary>
                    </span>
        </p>
      </div>
      <div v-if="rentals.length !== 0"
           class="rounded-md border p-4 mt-4">
        <div class="flex justify-between">
          <h2 class="text-2xl">
            Your rentals:
          </h2>
          <RefreshCcw 
              @click="refreshRentals"
              class="inline-block text-slate-300 hover:text-slate-400"/>
        </div>
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
              {{ rental.departmentCity }}
              <span class="block md:inline md:ml-4">
                <ButtonSecondary @click="handleCancelRent(rental.id)">
                  Cancel
                </ButtonSecondary>
              </span>
            </div>
          </li>
        </ul>
      </div>
      <div v-if="userData?.role === 'admin'"
           class="rounded-md border p-4 mt-4">
        <div class="flex justify-between">
          <h2 class="text-2xl">
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
                <InputPrimary
                    compact
                    type="text"
                    id="countryCode"
                    maxlength="2"
                    required
                    class="w-10 border rounded-full m-2 ml-0 focus:outline-none focus:border-orange-600 px-2"
                    v-model="newDepartment.countryCode"/>
              </label>
              <label for="city">
                City:
                <InputPrimary
                    compact
                    type="text"
                    id="city"
                    required
                    class="w-32 m-2"
                    v-model="newDepartment.city"/>
              </label>
              <label for="address">
                Address:
                <InputPrimary
                    compact
                    type="text"
                    id="address"
                    required
                    class="w-32 m-2"
                    v-model="newDepartment.address"/>
              </label>
              <InputButtonSecondary
                  value="Add"
                  class="ml-2"/>
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
              <ButtonSecondary>Remove</ButtonSecondary>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>