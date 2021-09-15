<template>
  <div class="profile-page container-fluid">
    <div class="row m-3">
      <div class="col-md-2">
        {{ profile.picture }}
      </div>
      <div class="col-md-10">
        <h3 class="shadow">
          {{ state.profile.name }}
        </h3>
        <h5 class="shadow">
          Vaults: {{ state.vaults.length }}
        </h5>
        <h5 class="shadow">
          Keeps: {{ state.keeps.length }}
        </h5>
      </div>
    </div>
    <div class="row m-3">
      <h2>Vaults</h2>
      <button class="btn btn-outline-maroon" title="Create Vault" v-if="state.account.id === state.profile.id" data-toggle="modal" data-target="#createVaultModal">
        <i class="mdi mdi-24px mdi-plus"></i>
      </button>
    </div>
    <div class="row m-1">
      <Vault v-for="vault in state.vaults" :key="vault.id" :vault="vault" />
    </div>
    <div class="row m-3">
      <h2>Keeps</h2>
      <button class="btn btn-outline-maroon" title="Create Keep" v-if="state.account.id === state.profile.id" data-toggle="modal" data-target="#createKeepModal">
        <i class="mdi mdi-24px mdi-plus"></i>
      </button>
    </div>
    <div class="masonry flex-wrap m-2">
      <Keep v-for="k in keeps" :key="k.id" :keep="k" class="keep" />
    </div>
    <keepModal />
    <createVaultModal />
    <createKeepModal />
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { profilesService } from '../services/ProfilesService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import KeepModal from '../components/KeepModal.vue'
import CreateVaultModal from '../components/CreateVaultModal.vue'
export default {
  components: { KeepModal, CreateVaultModal },
  setup() {
    const route = useRoute()
    const state = reactive({
      user: computed(() => AppState.user),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults)
      // account: computed(() => AppState.account) I shouldn't need account bc I'm using profile but I'm leaving it in case.
    })
    onMounted(async() => {
      try {
        await keepsService.getKeepsByProfileId(route.params.id)
        await vaultsService.getVaultsByProfileId(route.params.id)
        await profilesService.getProfileById(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
