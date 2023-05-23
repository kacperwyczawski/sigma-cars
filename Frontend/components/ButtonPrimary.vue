<script setup lang="ts">
defineEmits(['click']);
withDefaults(defineProps<{
  element?: "button" | "a";
  showArrow?: boolean;
}>(), {
  element: "button",
  showArrow: true
});
</script>
<template>
  <component
      :is="element"
      class="group px-3 py-2 hover:bg-orange-600 text-center text-white bg-slate-800 transition-colors duration-75 rounded-md text-base"
      @click.prevent="$emit('click')">
    <slot></slot>
    <span
        v-if="showArrow"
        class="group-hover:translate-x-2 inline-block transition-transform">
            -&gt;
        </span>
  </component>
</template>