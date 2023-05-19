<!--suppress CssUnusedSymbol -->
<template>
    <TheHeader/>
    <NuxtPage/>
</template>
<style>
.page-enter-active,
.page-leave-active {
    transition: all 0.1s;
}

.page-enter-from {
    transform: translateY(.5rem);
    opacity: 0;
}

.page-leave-to {
    transform: translateY(-.5rem);
    opacity: 0;
}
</style>