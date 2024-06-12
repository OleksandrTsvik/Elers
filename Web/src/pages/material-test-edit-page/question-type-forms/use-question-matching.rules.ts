import { useTranslation } from 'react-i18next';

import { TestQuestionMatchOption } from '../../../models/test-question.interface';

import type { ValidatorRule } from 'rc-field-form/lib/interface';

interface Rules {
  options: ValidatorRule[];
}

export default function useQuestionMatchingRules(): Rules {
  const { t } = useTranslation();

  return {
    options: [
      {
        validator: async (_, options?: Partial<TestQuestionMatchOption>[]) => {
          if (
            !options ||
            !Array.isArray(options) ||
            options.length < 2 ||
            options.filter((item) => !item || item.question).length < 2
          ) {
            return Promise.reject(new Error(t('course_test.add_two_matching')));
          }

          if (options.some((item) => !item || !item.answer)) {
            return Promise.reject(
              new Error(t('course_test.enter_all_answers')),
            );
          }

          return Promise.resolve();
        },
      },
    ],
  };
}
