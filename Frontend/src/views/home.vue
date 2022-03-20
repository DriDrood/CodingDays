<template>
    <div class="home">
        <h1>Coding days</h1>
        <router-link :to="{ name: 'invitation' }">Pozvánka / informace</router-link>
        <router-link :to="{ name: 'register' }">Registrace</router-link>
        <router-link :to="{ name: 'help' }">Nápovědy</router-link>
    </div>
</template>

<script>
export default {
    name: "home"
}
</script>