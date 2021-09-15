import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    logger.log('keeps', res.data)
    AppState.keeps = res.data
  }

  async getKeepsById(profileId) {
    const res = await api.get('api/profiles/' + profileId + '/keeps')
    AppState.keeps = res.data
  }

  async createKeep(newKeep) {
    const res = await api.post('api/keeps', newKeep)
    logger.log(res.data)
  }

  async deleteKeep(id) {
    await api.delete('api/keeps/' + id)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
  }

  async getKeepsByVaultId(vaultId) {
    const res = await api.get('api/vaults/' + vaultId + 'keeps')
    AppState.keeps = res.data
  }

  setActiveKeep(id) {
    const keep = AppState.keeps.find(k => k.id === id)
    AppState.activeKeep = keep
  }
}
export const KeepsService = newKeepsService()