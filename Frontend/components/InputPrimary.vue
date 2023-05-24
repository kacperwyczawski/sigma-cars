<script setup>
const model = defineModel();
</script>
<template>
  <input
      v-model="model"
      class="border rounded-md py-2 px-3 text-gray-700 leading-tight focus:outline outline-orange-400 outline-2">
</template>