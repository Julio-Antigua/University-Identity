<template>

    <student></student>

</template>

<script>
import student from '../../components/Add/StudentAddComponent.vue'
export default{
    name:'studentView',
    props:{
        msg:String
    },
    components:{
        student
    }
}

</script>

<style>

</style>