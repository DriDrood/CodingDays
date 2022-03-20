<template>
  <div class="help">
    <div class="picker">
      <span v-for="index in count" :key="`i-${index}`">{{ result[index - 1] ?? "_" }}</span>
      <button @click="select">Vlož {{ current }}</button>
      <button @click="reset">Reset</button>
      <button @click="send">Send</button>
    </div>
    <div class="response"></div>
  </div>
</template>

<script>
export default {
  name: "help",
  props: {
    count: {
      type: Number,
      default: 3,
    },
  },
  data: () => ({
    result: [],
    current: 0,
    sent: null,
  }),
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
.help {
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
