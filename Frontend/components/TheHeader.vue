<template>
    <header class="flex bg-orange-600 p-2 sm:p-4 justify-between items-center text-white">
        <div class="flex gap-2 text-xl sm:text-2xl font-bold">
            <NuxtLink class="bg-slate-800 w-8 h-8 grid place-items-center rounded-md"
                      href="/">
                &Sigma;
            </NuxtLink>
            <NuxtLink class="leading-8"
                      href="/">
                Sigma Cars
            </NuxtLink>
        </div>
        <div class="flex-row font-normal text-lg sm:text-xl divide-x-2">
            <NuxtLink href="/" class="px-2">
                Log in
            </NuxtLink>
            <NuxtLink href="/" class="px-2">
                Sign up
            </NuxtLink>

        </div>
    </header>
</template>