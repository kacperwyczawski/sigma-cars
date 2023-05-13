<script setup>
</script>
<template>
    <div class="min-h-[calc(100vh-4rem)] grid place-items-center bg-gray-100 p-2">
        <div class="max-w-md w-full divide-y bg-white border rounded-md">
            <div class="p-4">
                <h1 class="text-2xl text-center">Log in</h1>
            </div>
            <form class="p-4">
                <div class="mb-4">
                    <label class="block text-gray-800 font-bold mb-2" for="email">
                        Email
                    </label>
                    <input
                            class="border rounded-md w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none"
                            id="email"
                            type="email"
                            placeholder="Email address"
                            required
                    >
                </div>
                <div class="mb-4">
                    <label class="block text-gray-800 font-bold mb-2" for="password">
                        Password
                    </label>
                    <input
                            class="border rounded-md w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none"
                            id="password"
                            type="password"
                            placeholder="Password"
                            required
                    >
                </div>
                <div class="flex items-center justify-between">
                    <ButtonArrow @click="HandleLogIn">
                        Log in
                    </ButtonArrow>
                    <NuxtLink class="text-sm"
                              to="/signup"
                    >
                        Are you new here? Sign up!
                    </NuxtLink>
                </div>
            </form>
        </div>
    </div>
</template>