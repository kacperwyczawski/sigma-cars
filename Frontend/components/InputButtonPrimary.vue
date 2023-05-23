<script setup lang="ts">
defineEmits(['click']);
withDefaults(defineProps<{
  type?: 'submit' | 'button' | 'reset';
  value?: string;
}>(), {
  type: 'submit',
  value: 'Submit'
});
</script>
<template>
  <input
      :type="type"
      :value="value"
      class="group px-3 py-2 hover:bg-orange-600 text-center text-white bg-slate-800 transition-colors duration-75 rounded-md text-base"
      @click="$emit('click')">
</template>