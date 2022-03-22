<template>
  <div class="error">
    <h1>Ajaj, chybka</h1>
    <h2>
        <p>Můžeš to zkusit <a href="/" @click="back">znovu</a>, třeba to i pomůže.</p>
        <p>(Pravděpodobně ne... Kdyby ti to fakt nešlo, tak napiš mail.)</p>
    </h2>
  </div>
</template>

<script>
export default {
  name: 'error',
  methods: {
    back(ev) {
      ev.preventDefault();
      window.history.back();
    }
  }
}
</script>