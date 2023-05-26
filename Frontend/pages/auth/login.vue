<script setup lang="ts">
import {UserData} from "~/types/UserData";

const router = useRouter();
const userData = useUserData();

const state = reactive({
  email: '',
  password: '',
});

async function HandleLogIn() {
  const {data} = await useFetch("/api/auth/login", {
    method: "POST",
    body: {
      email: state.email,
      password: state.password,
    },
  });
  userData.value = data.value as UserData;
  await router.push("/");
}

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
          <InputPrimary
              v-model="state.email"
              class="w-full"
              id="email"
              type="email"
              placeholder="Email address"
              required/>
        </div>
        <div class="mb-4">
          <label class="block text-gray-800 font-bold mb-2" for="password">
            Password
          </label>
          <InputPrimary
              v-model="state.password"
              class="w-full"
              id="password"
              type="password"
              placeholder="Password"
              required/>
        </div>
        <div class="flex items-center justify-between">
          <ButtonPrimary @click="HandleLogIn">
            Log in
          </ButtonPrimary>
          <NuxtLink class="text-sm"
                    to="/auth/signup"
          >
            Are you new here? Sign up!
          </NuxtLink>
        </div>
      </form>
    </div>
  </div>
</template>