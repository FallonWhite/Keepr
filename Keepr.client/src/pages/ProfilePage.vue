<template>
  <div class="profile-page container-fluid">
    <div class="row">
      <div class="col-md-2">
        {{ profile.picture }}
      </div>
      <div class="col-md-10">
        {{ profile.name }} <br>
        Vaults: {{ state.vaults.length }}<br>
        Keeps: {{ state.keeps.length }}
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    const router = useRoute()
    onMounted(async() => {
      try {
        await keepsService.getAll({ creatorId: router.params.id })
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style lang="scss" scoped>
</style>
