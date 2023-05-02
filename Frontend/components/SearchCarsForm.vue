<script setup>
import {Listbox, ListboxButton, ListboxOption, ListboxOptions} from "@headlessui/vue";
import {Calendar, CheckIcon, ChevronsUpDown} from "lucide-vue-next";

const departments = [
    "Boston",
    "Chicago",
    "Paris",
    "London",
    "Warsaw",
    "Sydney",
    "Tokyo",
    "New York",
];

const defaultStartDate = new Date();
const defaultEndDate = new Date();
defaultEndDate.setDate(defaultEndDate.getDate() + 7);

const selectedDepartment = ref(departments[0]);
const startDate = ref(defaultStartDate.toISOString().slice(0, 10));
const endDate = ref(defaultEndDate.toISOString().slice(0, 10));
</script>
<template>
    <form class="max-w-3xl flex gap-3 items-end flex-grow flex-wrap">
        <label class="flex-grow relative">
            From:
            <input v-model="startDate"
                    class="block border w-full py-2 px-3 rounded-md h-10 mt-2 bg-white"
                    type="date"
                    max="01-01-2200"
                    min="01-01-2000"/>
            <span class="pointer-events-none absolute right-4 bottom-2.5 bg-white">
                <Calendar class="text-slate-800 h-5 w-5"/>
            </span>
        </label>
        <label class="flex-grow relative">
            To:
            <input v-model="endDate"
                    class="block border w-full py-2 px-3 rounded-md h-10 mt-2 bg-white"
                    type="date"
                    max="01-01-2200"
                    min="01-01-2000"/>
            <span class="pointer-events-none absolute right-4 bottom-2.5 bg-white">
                <Calendar class="text-slate-800 h-5 w-5"/>
            </span>
        </label>
        <label class="flex-grow">
            Location:
            <span class="w-full">
                <Listbox v-model="selectedDepartment">
                    <div class="relative mt-1">
                        <ListboxButton
                                class="block border w-full py-2 px-3 rounded-md h-10 mt-2 pr-16 bg-white text-left">
                            <span class="block truncate">
                                {{ selectedDepartment }}
                            </span>
                            <span class="pointer-events-none absolute inset-y-0 right-0 flex items-center pr-2">
                                <ChevronsUpDown class="h-5 w-5 text-slate-800"
                                                aria-hidden="true"/>
                            </span>
                        </ListboxButton>
                        <Transition
                                leave-active-class="transition duration-100 ease-in"
                                leave-from-class="opacity-100"
                                leave-to-class="opacity-0">
                            <ListboxOptions
                                    class="absolute mt-1 max-h-60 w-full overflow-auto rounded-md bg-white py-1 text-base shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none sm:text-sm">
                                <ListboxOption
                                        v-slot="{ active, selected }"
                                        v-for="department in departments"
                                        :key="department"
                                        :value="department"
                                        class="relative cursor-default select-none py-2 pl-10 pr-4 hover:bg-slate-100"
                                        as="template">
                                    <li class="relative cursor-default select-none py-2 pl-10 pr-4]">
                                        <span :class="[ selected ? 'font-semibold' : 'font-normal', 'block truncate',]">
                                            {{ department }}
                                        </span>
                                        <span v-if="selected"
                                              class="absolute inset-y-0 left-0 flex items-center pl-3 text-orange-600">
                                            <CheckIcon class="h-5 w-5" aria-hidden="true"/>
                                        </span>
                                    </li>
                                </ListboxOption>
                            </ListboxOptions>
                        </Transition>
                    </div>
                </Listbox>
            </span>
        </label>
        <input
                class="block border py-2 px-3 rounded-md h-10 flex-grow bg-slate-800 text-white border-none hover:bg-slate-700"
                type="submit" value="Explore!"/>
    </form>
</template>