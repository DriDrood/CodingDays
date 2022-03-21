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
    display: grid;
    position: absolute;
    top: 0;
    left: 50%;
    transform: translateX(-50%);
    z-index: 100;
    
    width: 10rem;
    height: 5rem;
    justify-content: center;
    align-content: center;
    
    padding: 0;
    border-radius: 0 0 1rem 1rem;
    background-color: hsla(0, 0%, 35%, .8);
}
#app .notifications {
    position: absolute;
    top: 0;
    left: 50%;
    transform: translateX(-50%);
    z-index: 50;

    width: 60%;
    height: auto;

    padding: 0;
    border-radius: 0 0 1rem 1rem;
    overflow: hidden;

    div {
        width: 100%;
        padding: .5rem 1rem;
        text-align: center;
        
        &.success {
            background-color: hsla(118, 44%, 45%, .8);
        }
        &.warning {
            background-color: hsla(56, 44%, 45%, .8);
        }
        &.error {
            background-color: hsla(0, 44%, 45%, .8);
        }
        
        .close {
            cursor: pointer;
            float: right;
        }
    }
}
</style>