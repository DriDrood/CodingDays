<template>
  <div class="hint">
    <router-link :to="{ name: 'home' }">Zpět</router-link>
    <div class="picker">
      <span v-for="index in count" :key="`i-${index}`">{{ result[index - 1] ?? "_" }}</span>
      <button @click="select">Vlož {{ current }}</button>
      <button @click="reset">Reset</button>
      <button @click="send">Send</button>
    </div>
    <div class="response">{{ hint.response.text }}</div>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: "hint",
  props: {
    count: {
      type: Number,
      default: 8,
    },
  },
  data: () => ({
    result: [],
    current: 0,
    sent: null,
    response: null,
  }),
  computed: {
    ...mapState(['hint'])
  },
  methods: {
    select() {
      if (this.result.length >= this.count)
        return;

      this.result.push(this.current);
    },
    reset() {
      this.result = [];
    },
    send() {
      this.$store.dispatch('sentHelpCode', { code: this.result.join(".") })
    },
  },
  mounted() {
    setInterval(() => {
      this.current = Math.floor(Math.random() * 10);
    }, 600);
  },
};
</script>

<style lang="scss">
.hint {
  .picker {
    display: grid;
    padding: 3rem;
    grid-auto-flow: column;
    align-content: center;
    align-items: center;
    justify-content: center;
    grid-gap: 1rem;
  }
}
</style>
