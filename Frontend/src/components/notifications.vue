<template>
    <div v-if="notifications.loading" class="loading">Loading...</div>
    <div class="notifications">
        <div v-for="(notification, id) in notifications.items" :key="id" :class="notification.type">
            {{ notification.text }}
            <span class="close" @click="close(id)">X</span>
        </div>
    </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
    name: 'notifications',
    computed: {
        ...mapState(['notifications'])
    },
    methods: {
        close(id) {
            this.$store.commit('notificationsRemove', { id })
        }
    }
}
</script>

<style lang="scss">
#app .loading {
    width: 10rem;
    height: 5rem;
    position: absolute;
    left: 50%;
    transform: translateX(-50%);
    z-index: 100;
    
    padding: 0;
    border-radius: 0 0 1rem 1rem;
    background-color: hsl(0, 0%, 35%);
}
#app .notifications {
    width: 60%;
    height: auto;
    position: absolute;
    left: 50%;
    transform: translateX(-50%);
    z-index: 50;

    padding: 0;
    border-radius: 0 0 1rem 1rem;

    div {
        width: 100%;
        padding: .5rem 1rem;
        text-align: center;
        
        &.success {
            background-color: hsl(118, 44%, 45%);
        }
        &.warning {
            background-color: hsl(56, 44%, 45%);
        }
        &.error {
            background-color: hsl(0, 44%, 45%);
        }
        
        .close {
            cursor: pointer;
            float: right;
        }
    }
}
</style>