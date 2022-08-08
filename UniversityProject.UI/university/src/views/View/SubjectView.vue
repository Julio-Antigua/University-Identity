<template>

    <SubjectViewComponent></SubjectViewComponent>

</template>

<script>
import SubjectViewComponent from '../../components/View/SubjectViewComponent.vue'
export default{
    name: 'SubjectView',
    props:{
        msg: String
    },
    components:{
        SubjectViewComponent
    }
}
</script>

<style>

</style>