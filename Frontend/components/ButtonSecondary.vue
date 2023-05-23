<script setup lang="ts">
defineEmits(['click']);
withDefaults(defineProps<{
  element?: "button" | "a";
}>(), {
  element: "button"
});
</script>
<template>
  <component
      :is="element"
      class="underline text-orange-600 hover:text-orange-800"
      @click.prevent="$emit('click')">
    <slot></slot>
  </component>
</template>