<script setup>
import {User, SlidersHorizontal} from "lucide-vue-next";
import {useUserData} from "~/composables/useUserData";
import ButtonPrimary from "~/components/ButtonPrimary.vue";

const props = defineProps({
  car: Object,
});

const userData = useUserData();
const router = useRouter();
const route = useRoute();

const startDate = computed(() => {
  let date = new Date(route.query.startDate);
  date.setHours(10);
  return date.toISOString();
});

const endDate = computed(() => {
  let date = new Date(route.query.endDate);
  date.setHours(10);
  return date.toISOString();
});

function handleRent() {
  if (!userData.value) {
    // TODO: Tell user they need to be logged in via modal or something
    router.push('/auth/login');
    return;
  }

  useFetch(`/api/car-types/${props.car.id}/rentals`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: {
      userId: userData.value.userId,
      startDate: startDate.value,
      endDate: endDate.value,
    },
  });

  router.push('/profile');
}

const showDetails = ref(false);

</script>
<template>
  <div class="border divide-y rounded-md">
    <div class="flex justify-between text-slate-800 font-bold text-2xl p-2">
      <h1>{{ car.make }} {{ car.model }}</h1>
      <div>${{ car.pricePerDay }}</div>
    </div>
    <div class="p-2">
      <div class="aspect-video bg-slate-200 animate-pulse rounded-md"/>
    </div>
    <div class="text-slate-600 text-xl p-2 flex justify-between items-center">
      <div class="flex gap-1">
        <User class="h-7"/>
        {{ car.seatCount }} seats
      </div>
      <div class="flex gap-2 items-center">
        <button
            v-if="userData && userData.role === 'admin'"
            @click="showDetails = true"
            class="bg-slate-100 rounded basis-6 p-2 hover:bg-slate-200 transition-colors duration-75 block">
          <SlidersHorizontal/>
          <Modal :show="showDetails">
            <template #header>
              Car type details
            </template>
            <template #body>
              <CarTypeDetails :carTypeId="car.id"/>
            </template>
            <template #footer>
              <ButtonPrimary
                  :show-arrow="false">
                Delete
              </ButtonPrimary>
              <ButtonPrimary
                  @click="showDetails = false"
                  :show-arrow="false">
                Close
              </ButtonPrimary>
            </template>
          </Modal>
        </button>
        <ButtonPrimary @click="handleRent">
          Rent now
        </ButtonPrimary>
      </div>
    </div>
  </div>
</template>