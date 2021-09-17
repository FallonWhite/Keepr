<template>
  <div class="row">
    <div class="col-md-12">
      <h2 class="p-3">
        {{ vault.name }}
      </h2>

      <h3 class="mx-3">
        Keeps: {{ keeps.length }}
      </h3>
    </div>

    <div class="row d-flex mx-4">
      <div class="card-columns m-1">
        <VaultKeep v-for="k in keeps" :key="k.id" :keep="k" class="keep" />
      </div>
    </div>
  </div>
</template>

<script>

import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'

export default {

  setup() {
    const route = useRoute()
    onMounted(() => {
      vaultsService.getVaultById(route.params.id)
      keepsService.getKeepsByVaultId(route.params.id)
    })
    const state = reactive({
    })

    return {
      state,
      activeVault: computed(() => AppState.activeVault),
      vault: computed(() => AppState.vault),
      keeps: computed(() => AppState.keeps)
    }
  }

}

</script>

<style scoped lang="scss">

body {
 margin: 0;
 padding: 1rem;
}

 .keep {
    width: 150px;
    background-size: cover;
    background-color: black;
    color: black;
  }

</style>
