<template>
  <div class="global-image">
    <div class="rounded-box">
      <div class="input-fields">
        <div class="input-group">
          <label>Profile number</label>
          <dx-text-box v-model="profile" :maxLength="13"></dx-text-box>
        </div>
        <div class="date-input">
          <label>From</label>
          <dx-date-box v-model="from"></dx-date-box>
        </div>
        <div class="date-input">
          <label>To</label>
          <dx-date-box v-model="to"></dx-date-box>
        </div>
      </div>
      <button @click="search" :disabled="!isValid">Search</button>
    </div>
    <dx-data-grid :dataSource="gridData">
      <!-- Add column definitions and other grid settings here -->
    </dx-data-grid>
  </div>
</template>

<script>
import axios from 'axios';
import { DxDateBox, DxDataGrid, DxTextBox } from 'devextreme-vue';

export default {
  components: {
    DxDateBox,
    DxDataGrid,
    DxTextBox
  },
  data() {
    return {
      profile: '',
      from: null,
      to: null,
      gridData: []
    };
  },
  computed: {
    isValid() {
      return this.profile && (!this.from === !this.to);
    }
  },
  methods: {
    async search() {
      if (this.isValid) {
        const response = await axios.get('/api/transactions', {
          params: {
            profile: this.profile,
            from: this.from ? this.from.toISOString() : null,
            to: this.to ? this.to.toISOString() : null
          }
        });
        this.gridData = response.data;
      }
    }
  }
};
</script>

<style scoped>
.rounded-box {
  border: 1px solid #ccc;
  border-radius: 12px;
  padding: 10px;
  margin-bottom: 10px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

.input-fields {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 10px;  /* Optional: Adds some space between the input fields and the search button */
}

.input-group, .date-input {
  display: flex;
  flex-direction: column;
}

.global-image {
  z-index: 2;  
}
</style>
