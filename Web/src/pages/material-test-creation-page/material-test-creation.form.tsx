import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useCreateCourseMaterialTestMutation } from '../../api/course-materials.mutations.api';
import { GradingMethod } from '../../models/course-material.type';
import { MaterialTestForm, MaterialTestFormValues } from '../../shared';

interface Props {
  tabId: string;
}

export default function MaterialTestCreationForm({ tabId }: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [createCourseMaterial, { isLoading, error }] =
    useCreateCourseMaterialTestMutation();

  const handleSubmit = async (values: MaterialTestFormValues) => {
    await createCourseMaterial({ tabId, ...values })
      .unwrap()
      .then((testId) =>
        navigate(`/courses/material/edit/${tabId}/test/${testId}`),
      );
  };

  return (
    <MaterialTestForm
      initialValues={{
        title: '',
        numberAttempts: 1,
        gradingMethod: GradingMethod.BestAttempt,
        shuffleQuestions: false,
      }}
      textOnSubmitButton={t('actions.add')}
      isLoading={isLoading}
      error={error}
      onSubmit={handleSubmit}
    />
  );
}
