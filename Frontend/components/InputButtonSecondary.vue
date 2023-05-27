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
      class="underline text-orange-600 hover:text-orange-800"
      @click="$emit('click')">
</template>