<template>
    <ResetPassword v-if="isReset" />
    <Login v-else-if="!isLogged" />
    <Dashboard v-else />
</template>

<script>
import ResetPassword from './components/resetPassword.vue'
import Login from './components/login.vue'
import Dashboard from './components/dashboard.vue'

export default {
    name: 'app',
    components: {
        ResetPassword,
        Login,
        Dashboard,
    },
    computed: {
        isReset() {
            return this.$store.state.resetPasswordToken != null
        },
        isLogged() {
            return this.$store.state.token != null
        },
    },
    mounted() {
        if (location.pathname.toLowerCase() == "/resetpassword") {
            const params = new URLSearchParams(location.search)
            this.$store.commit('forgot', { token: params.get('token') })
        }
    }
}
</script>

<style lang="scss">
* {
    padding: 0;
    margin: 0;
}
html, body, #app {
    width: 100%;
    height: 100%;
}

#app {
    display: grid;
    justify-content: center;
    align-content: center;
    grid-gap: .5rem;
}
</style>
