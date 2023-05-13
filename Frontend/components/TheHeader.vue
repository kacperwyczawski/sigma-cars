<script setup>

import {User} from "lucide-vue-next";

const userData = useUserData();

</script>
<template>
    <header class="flex bg-orange-600 p-2 sm:p-4 justify-between items-center text-white h-16">
        <div class="flex gap-2 text-xl sm:text-2xl font-bold">
            <NuxtLink class="w-8 h-8"
                      href="/">
                <img src="/favicon.svg" alt="logo">
            </NuxtLink>
            <NuxtLink class="leading-8"
                      href="/">
                Sigma Cars
            </NuxtLink>
        </div>
        <div class="flex-row font-normal text-md divide-x border rounded-md divide-gray-200">
            <NuxtLink v-if="!userData"
                      href="/login"
                      class="px-3 py-1.5 inline-block hover:bg-white/20">
                Log in
            </NuxtLink>
            <div v-else
                 class="px-3 py-1.5 h-full inline-block hover:bg-white/20">
                <User class="h-5 inline scale-125"/>
            </div>
            <NuxtLink v-if="!userData"
                      href="/signup"
                      class="px-3 py-1.5 inline-block hover:bg-white/20">
                Sign up
            </NuxtLink>
            <div v-else
                 class="px-3 py-1.5 inline-block font-semibold">
                Hello, {{ userData.firstName }}!
            </div>
        </div>
    </header>
</template>