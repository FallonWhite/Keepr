<template>
  <div v-if="vault.isPrivate == false" class="m-1 vaultAction">
    <router-link :to="{name: 'Vault', params: {id: vault.id}}" @click="setActiveVault">
      <div class="Vault card m-2 shadow">
        <div class="my-2 p-2">
          <h5 class="vault-name text-primary text-center">
          </h5>
        </div>
      </div>
    </router-link>

    <div>
      <p>{{ vault.name }}</p>
    </div>

    <div class="d-flex justify-content-center">
      <button @click="deleteVault(vault.id)" v-if="vault.creatorId === account.id" class="btn btn-danger shadow" title="Delete Vault">
        Delete
      </button>
    </div>
  </div>

  <div v-if="vault.isPrivate === true" class="vault-bg m-1">
    <router-link :to="{name: 'Vault', params: {id: vault.id}}" @click="setActiveVault">
      <div class="Vault card m-2 shadow">
        <div class="my-2 p-2">
          <div class="vault-name text-center text-warning">
            <h5>Private Vault</h5>
          </div>
        </div>
      </div>
    </router-link>

    <div>
      <h5>{{ vault.name }}</h5>
    </div>

    <div class="d-flex justify-content-center">
      <button @click="deleteVault(vault.id)" v-if="vault.creatorId === account.id" class="btn btn-outline-dark btn-danger shadow" title="Delete Vault">
        Delete
      </button>
    </div>
  </div>
</template>

<script>

import { computed, reactive } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'

export default {

  props: {
    vault: { type: Object, required: true }
  },

  setup(props) {
    const state = reactive({
    })
    return {
      state,
      account: computed(() => AppState.account),
      async deleteVault(id) {
        if (confirm('Are you sure?')) {
          await vaultsService.deleteVault(id)
        }
      },

      setActiveVault() {
        vaultsService.setActiveVault(props.vault.id)
      }
    }
  }

}

</script>

<style scoped>

.Vault {
 background-image: url("../assets/img/vault.png");
 background-size: cover;
}

.vaultAction:hover {
 transform: scale(1.1);
 }

</style>
