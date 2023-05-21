<script setup>
defineEmits(['click']);
</script>
<template>
    <button class="underline text-orange-600 hover:text-orange-800"
            @click.prevent="$emit('click')">
        <slot></slot>
    </button>
</template>