<template>
    {{ dashboard }}
</template>

<script>
import { mapState } from 'vuex'

export default {
    name: 'data',
    computed: {
        ...mapState(['dashboard']),
    },
    mounted() {
        this.$store.dispatch('loadDashboard')
    },
}
</script>