#pragma once
#include "Stdafx.h"
#include <stdlib.h>
#include "Rifle.h"

ShotInfo^ Rifle::SolveShot(
	double shootingAngle,
	double windSpeed,
	double windAngle,
	int range,
	AtmosphericInfo^ atmInfo) {

	ShotInfo^ shotInfo = gcnew ShotInfo();

	double* solution;

	double bc = ammoInfo->BC;
	if (atmInfo != nullptr)
	{
		bc = AtmCorrect(ammoInfo->BC,
			rifleInfo->ZeroingConditions->Altitude,
			rifleInfo->ZeroingConditions->Barometer,
			rifleInfo->ZeroingConditions->Temperature,
			rifleInfo->ZeroingConditions->RelativeHumidity);
	}

	try {
		SolveAll((int)ammoInfo->DragFunction,
			bc,
			ammoInfo->MuzzleVelocity, 
			scopeInfo->Height,
			shootingAngle,
			zerAngle,
			windSpeed, 
			windAngle,
			&solution);

		double elevationClickMultiplier = 1 / scopeInfo->ElevationClicksPerMOA;
		double windageClickMultiplier = 1 / scopeInfo->WindageClicksPerMOA;

		shotInfo->ElevationMOA = GetMOA(solution, range);
		shotInfo->WindageMOA = GetWindageMOA(solution, range);
		shotInfo->TimeToTargetSec = GetTime(solution, range);
		shotInfo->ImpactVelocity = GetVelocity(solution, range); 
		shotInfo->ElevationClicks = shotInfo->ElevationMOA * elevationClickMultiplier;
		shotInfo->WindageClicks = shotInfo->WindageMOA * windageClickMultiplier;
		shotInfo->Range = range;
	}
	finally{
		free(solution);
	}

	return shotInfo;
}