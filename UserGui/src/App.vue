<template>
    <Login v-if="!isLogged" />
    <Dashboard v-else />
</template>

<script>
import Login from './components/login.vue'
import Dashboard from './components/dashboard.vue'

export default {
    name: 'app',
    components: {
        Login,
        Dashboard,
    },
    computed: {
        isLogged() {
            return this.$store.state.token != null
        },
    },
}
</script>

<style lang="scss">
</style>
