<template>
    <form @submit="send">
        <input type="text" v-model="name" placeholder="Username" />
        <input type="password" v-model="newPassword" placeholder="New password" />
        <button>Set new password</button>
    </form>
</template>

<script>
export default {
    name: 'resetPassword',
    data: () => ({
        name: '',
        newPassword: '',
    }),
    methods: {
        send(ev) {
            ev.preventDefault()
            
            this.$store.dispatch('resetPassword', { name: this.name, newPassword: this.newPassword })
        }
    }
}
</script>