<script setup lang="ts">
const model = defineModel();
const props = withDefaults(defineProps<{
  compact?: boolean;
}>(), {
  compact: false,
});
</script>
<template>
  <input
      v-model="model"
      class="border rounded-md text-gray-700 leading-tight focus:outline outline-orange-400 outline-2"
      :class="compact ? 'py-1 px-2' : 'py-2 px-3'">
</template>