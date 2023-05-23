<script setup>

const state = reactive({
    email: '',
    password: '',
    firstName: '',
    lastName: '',
});
const router = useRouter();
const userData = useUserData();

async function HandleLogIn() {
    const {data} = await useFetch("api/auth/register", {
        method: "POST",
        body: {
            email: state.email,
            password: state.password,
            firstName: state.firstName,
            lastName: state.lastName,
        },
    });
    userData.value = data.value;
    await router.push("/");
}

</script>
<template>
    <div class="min-h-[calc(100vh-4rem)] grid place-items-center bg-gray-100 p-2">
        <div class="max-w-md w-full divide-y bg-white border rounded-md">
            <div class="p-4">
                <h1 class="text-2xl text-center">Sign up</h1>
            </div>
            <form class="p-4">
                <div class="mb-4">
                    <label class="block text-gray-800 font-bold mb-2" for="firstName">
                        First name
                    </label>
                    <input
                            v-model="state.firstName"
                            class="border rounded-md w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none"
                            id="firstName"
                            type="email"
                            placeholder="First name"
                            required
                    >
                </div>
                <div class="mb-4">
                    <label class="block text-gray-800 font-bold mb-2" for="firstName">
                        Last name
                    </label>
                    <input
                            v-model="state.lastName"
                            class="border rounded-md w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none"
                            id="lastName"
                            type="email"
                            placeholder="Last name"
                            required
                    >
                </div>
                <div class="mb-4">
                    <label class="block text-gray-800 font-bold mb-2" for="email">
                        Email
                    </label>
                    <input
                            v-model="state.email"
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
                            v-model="state.password"
                            class="border rounded-md w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none"
                            id="password"
                            type="password"
                            placeholder="Password"
                            required
                    >
                </div>
                <div class="flex items-center justify-between">
                    <ButtonPrimary>
                        Sign up
                    </ButtonPrimary>
                    <NuxtLink class="text-sm"
                              to="/auth/login"
                    >
                        Already have an account? Log in!
                    </NuxtLink>
                </div>
            </form>
        </div>
    </div>
</template>