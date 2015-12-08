#pragma once
#include "Stdafx.h"
#include <stdlib.h>
#include "Rifle.h"

using namespace UnitsNet;
using namespace System::Collections::Generic;

ShotInfo^ Rifle::Solve(
	double shootingAngle,
	Speed windSpeed,
	double windAngle,
	Length range,
	AtmosphericInfo^ atmInfo) {

	ShotInfo^ shotInfo = gcnew ShotInfo();

	double* solution;

	double bc = ammoInfo->BC;
	if (atmInfo != nullptr)
	{
		bc = AtmCorrect(ammoInfo->BC,
			rifleInfo->ZeroingConditions->Altitude.Feet,
			rifleInfo->ZeroingConditions->Barometer.Psi * 2.03602, //convert to Hg,
			rifleInfo->ZeroingConditions->Temperature.DegreesFahrenheit,
			rifleInfo->ZeroingConditions->RelativeHumidity);
	}

	try {
		SolveAll((int)ammoInfo->DragFunction,
			bc,
			ammoInfo->MuzzleVelocity.FeetPerSecond, 
			scopeInfo->Height.Inches,
			shootingAngle,
			zeroAngle,
			windSpeed.MilesPerHour, 
			windAngle,
			&solution);

		double elevationClickMultiplier = 1 / scopeInfo->ElevationClicksPerMOA;
		double windageClickMultiplier = 1 / scopeInfo->WindageClicksPerMOA;
		
		shotInfo->BulletDrop = Length::FromInches(GetPath(solution, range.Yards));
		shotInfo->WindDrift = Length::FromInches(GetWindage(solution, range.Yards));
		shotInfo->ElevationMOA = GetMOA(solution, range.Yards);
		shotInfo->WindageMOA = GetWindageMOA(solution, range.Yards);
		shotInfo->TimeToTargetSec = GetTime(solution, range.Yards);
		shotInfo->ImpactVelocity = Speed::FromFeetPerSecond(GetVelocity(solution, range.Yards)); 
		shotInfo->ElevationClicks = shotInfo->ElevationMOA * elevationClickMultiplier;
		shotInfo->WindageClicks = shotInfo->WindageMOA * windageClickMultiplier;
		shotInfo->Range = range;
	}
	finally{
		free(solution);
	}

	return shotInfo;
}

IEnumerable<ShotInfo^>^ Rifle::SolveMultiple(double shootingAngle,
	Speed windSpeed,
	double windAngle,
	IEnumerable<Length>^ ranges,
	AtmosphericInfo^ atmInfo) {

	List<ShotInfo^>^ results = gcnew List<ShotInfo^>();

	for each(auto range in ranges)
		results->Add(Solve(shootingAngle, windSpeed, windAngle, range, atmInfo));

	return results;
}