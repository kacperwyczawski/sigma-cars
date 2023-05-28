<script setup lang="ts">
import {Listbox, ListboxButton, ListboxOption, ListboxOptions} from "@headlessui/vue";
import {Calendar, CheckIcon, ChevronsUpDown} from "lucide-vue-next";
import {Department} from "~/types/Department";

const router = useRouter();

const {data: departments} = await useFetch<Department[]>("/api/departments");

const isListboxDisabled = computed(
    () => departments?.value?.length === 1
);

const defaultStartDate = new Date();
const defaultEndDate = new Date();
defaultEndDate.setDate(defaultEndDate.getDate() + 7);

const state = reactive({
  selectedDepartment: departments?.value?.[0] ?? {city: "No deps. available"} as Department,
  startDate: defaultStartDate.toISOString().slice(0, 10),
  endDate: defaultEndDate.toISOString().slice(0, 10),
});

function handleSubmit() {
  router.push({
    name: "search",
    query: {
      startDate: state.startDate,
      endDate: state.endDate,
      department: state.selectedDepartment.departmentId,
    },
  });
}
</script>
<template>
  <form class="max-w-3xl flex gap-3 items-end flex-grow flex-wrap"
        @submit.prevent="handleSubmit">
    <label class="flex-grow relative basis-32">
      From:
      <input
          v-model="state.startDate"
          class="block border w-full py-2 px-3 rounded-md h-10 mt-2 bg-white"
          type="date"
          max="01-01-2200"
          min="01-01-2000"/>
      <span class="pointer-events-none touch-none absolute right-4 bottom-2.5 bg-white">
        <Calendar class="text-slate-800 h-5 w-5"/>
      </span>
    </label>
    <label class="flex-grow relative basis-32">
      To:
      <input
          v-model="state.endDate"
          class="block border w-full py-2 px-3 rounded-md h-10 mt-2 bg-white"
          type="date"
          max="01-01-2200"
          min="01-01-2000"/>
      <span class="pointer-events-none touch-none absolute right-4 bottom-2.5 bg-white">
          <Calendar class="text-slate-800 h-5 w-5"/>
      </span>
    </label>
    <label class="flex-grow basis-32">
      Location:
      <span class="w-full">
        <Listbox
            v-model="state.selectedDepartment"
            :disabled="isListboxDisabled"
            v-slot="{ disabled }">
                    <div class="relative mt-1">
                        <ListboxButton
                            class="block border w-full py-2 px-3 rounded-md h-10 mt-2 pr-16 bg-white text-left">
                            <span class="block truncate"
                                  :class="{'text-slate-400': disabled}">
                                {{ state.selectedDepartment.city }}
                            </span>
                            <span class="pointer-events-none absolute inset-y-0 right-0 flex items-center pr-2">
                                <ChevronsUpDown class="h-5 w-5"
                                                :class="[disabled ? 'text-slate-400' : 'text-slate-800']"
                                                aria-hidden="true"/>
                            </span>
                        </ListboxButton>
                        <transition
                            enter-active-class="transition duration-100 ease-out"
                            enter-from-class="transform scale-95 opacity-0"
                            enter-to-class="transform scale-100 opacity-100"
                            leave-active-class="transition duration-75 ease-out"
                            leave-from-class="transform scale-100 opacity-100"
                            leave-to-class="transform scale-95 opacity-0">
                            <ListboxOptions
                                class="absolute mt-1 max-h-60 w-full overflow-auto rounded-md bg-white py-1 text-base shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none sm:text-sm">
                                <ListboxOption
                                    v-for="department in (departments as Department[])"
                                    v-slot="{ active, selected }"
                                    :key="department.departmentId"
                                    :value="department"
                                    class="relative cursor-default select-none py-2 pl-10 pr-4 hover:bg-slate-100"
                                    as="template">
                                    <li class="relative cursor-default select-none py-2 pl-10 pr-4]">
                                        <span :class="[ selected ? 'font-semibold' : 'font-normal', 'block truncate']">
                                            {{ department.city }}
                                        </span>
                                        <span v-if="selected"
                                              class="absolute inset-y-0 left-0 flex items-center pl-3 text-orange-600">
                                            <CheckIcon class="h-5 w-5"
                                                       aria-hidden="true"/>
                                        </span>
                                    </li>
                                </ListboxOption>
                            </ListboxOptions>
                        </transition>
                    </div>
        </Listbox>
      </span>
    </label>
    <input
        class="block border py-2 px-3 rounded-md h-10 flex-grow bg-slate-800 text-white border-none hover:bg-slate-700"
        type="submit"
        value="Explore!"/>
  </form>
</template>