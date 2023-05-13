<script setup>
defineEmits(['click']);
</script>
<template>
    <button class="group px-5 py-2 hover:bg-orange-600 text-center text-white bg-slate-800 transition-colors duration-75 rounded-md text-base"
            @click.prevent="$emit('click')">
        <slot></slot>
        <span class="group-hover:translate-x-2 inline-block transition-transform">
            -&gt;
        </span>
    </button>
</template>