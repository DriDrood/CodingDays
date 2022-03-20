<template>
    <div class="invitation">
        <router-link :to="{ name: 'home' }">Zpět</router-link>
        <img src="/invitation.png" alt="invitation" />
    </div>
</template>

<script>
export default {
    name: 'invitation',
}
</script>

<style lang="scss">
.invitation {
    display: grid;
    box-sizing: border-box;
    padding: 1rem 0;
    grid-gap: 1rem;
    justify-content: center;
}
</style>