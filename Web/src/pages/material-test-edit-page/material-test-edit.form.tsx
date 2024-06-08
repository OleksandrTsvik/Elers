import { Skeleton, Spin } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import { useUpdateCourseMaterialTestMutation } from '../../api/course-materials.mutations.api';
import { useGetCourseMaterialTestQuery } from '../../api/course-materials.queries.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { MaterialTestForm, MaterialTestFormValues } from '../../shared';

export default function MaterialTestEditForm() {
  const { id } = useParams();
  const { t } = useTranslation();

  const { data, isLoading, isFetching, error } = useGetCourseMaterialTestQuery({
    id,
  });

  const [
    updateCourseMaterial,
    { isLoading: isLoadingUpdate, error: errorUpdate },
  ] = useUpdateCourseMaterialTestMutation();

  const handleSubmit = async (values: MaterialTestFormValues) => {
    await updateCourseMaterial({ id, ...values }).unwrap();
  };

  if (isLoading) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <Spin spinning={isFetching} tip={t('loading.changes')}>
      <MaterialTestForm
        initialValues={data}
        textOnSubmitButton={t('actions.save_changes')}
        isLoading={isLoadingUpdate}
        error={errorUpdate}
        onSubmit={handleSubmit}
      />
    </Spin>
  );
}
