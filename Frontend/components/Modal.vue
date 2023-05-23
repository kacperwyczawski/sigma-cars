<script setup lang="ts">
const props = defineProps<{
  show: boolean;
}>();
</script>
<template>
  <Teleport to="body">
    <Transition>
      <!--TODO: try to use v-if instead of v-show-->
      <div v-if="show"
           class="fixed inset-0 backdrop-blur-sm grid place-items-center">
        <div class="max-w-4xl m-2 bg-white border rounded-md divide-y">
          <div class="p-2 text-2xl font-bold">
            <slot name="header"></slot>
          </div>
          <div class="p-2">
            <slot name="body"></slot>
          </div>
          <div 
              v-if="$slots.footer"
              class="p-2 flex justify-end gap-2">
            <slot name="footer"></slot>
          </div>
        </div>
      </div>
    </Transition>
  </Teleport>
</template>
<style scoped>
.v-enter-active,
.v-leave-active {
  transition: all 0.1s;
}

.v-enter-from {
  opacity: 0;
  transform: translateY(.5rem);
}

.v-leave-to {
  opacity: 0;
  transform: translateY(-.5rem);
}
</style>